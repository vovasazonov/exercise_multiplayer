using Attacks.Presenters;
using Descriptions;
using HealthPoints.Presenters;

public class PresenterManager
{
    private readonly HealthPointTextPresenter _enemyHealthPointTextPresenter;
    private readonly AttacksPresenter _attacksPresenter;

    public PresenterManager(ViewContainer viewContainer, ModelManager modelManager, Database database)
    {
        _enemyHealthPointTextPresenter = new HealthPointTextPresenter(viewContainer.EnemyHealthPointTextView, modelManager.EnemyModel.HealthPoint);
        _attacksPresenter = new AttacksPresenter(viewContainer.AttackButtonViewList, database.DescriptionsWeapon, modelManager.WeaponModels.Values, modelManager.EnemyModel);
    }

    public void Activate()
    {
        _enemyHealthPointTextPresenter.Activate();
        _attacksPresenter.Activate();
    }

    public void Deactivate()
    {
        _enemyHealthPointTextPresenter.Deactivate();
        _attacksPresenter.Deactivate();
    }
}